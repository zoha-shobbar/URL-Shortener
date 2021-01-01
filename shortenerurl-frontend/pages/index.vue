
<style>
.textarea_box {
  border: 1.5px dashed #1e70b9;
  background: #fbfbfb;
  margin-bottom: 10px;
  overflow: hidden;
}
.inner-box {
  height: 210px;
  background: #fff;
  border: 1px solid #1e70b96e;
  display: flex !important;
  align-items: center;
}
.inner-box .input-container {
  margin: auto;
}
.lbl {
  font-size: 14px;
}
.v-input__prepend-inner .v-input__prepend-inner {
  margin-top: 15px;
}
.inner-divider {
  border-color: #1e70b9 !important;
  margin-left: 5px !important;
}
.v-text-field--outlined fieldset {
  border-color: #e9e9e9;
}
.pt-01 {
  padding-top: 2px !important;
}
.blue-kind {
  color: #1e70b9;
}
</style>

<template>
  <v-container>
    <v-row class="text-center">
      <v-col class="pb-12"
        ><h1 class="blue-kind">Short links, big results</h1>
        <h3 class="blue-kind">Create your own Link</h3></v-col
      >
    </v-row>
    <v-row>
      <v-col class="col pa-0 pa-md-4">
        <div class="textarea_box pa-2 pa-md-3">
          <div class="inner-box">
            <v-col cols="11" md="10" class="input-container">
              <v-row class="pt-3 pt-sm-0"
                ><p class="text-caption">Enter your URL</p></v-row
              >
              <v-form v-model="formIsValid">
                <v-row>
                  <v-col cols="12" md="9" class="pa-0">
                    <v-text-field
                      den
                      outlined
                      background-color="#fbfbfb"
                      prefix=" "
                      placeholder="Enter Url"
                      :rules="inputRules"
                      v-model="urlString"
                    >
                      <template v-slot:prepend-inner>
                        <div>
                          <v-icon color="#1E70B9" meddium
                            >mdi-link-variant
                          </v-icon>
                          <v-divider vertical class="inner-divider"></v-divider>
                        </div>
                      </template>
                    </v-text-field>
                  </v-col>
                  <v-col cols="12" md="3" class="pt-01">
                    <v-btn
                      tile
                      color="primary"
                      x-large
                      class="d-none d-sm-flex"
                      :disabled="!formIsValid"
                      :loading="loading.save"
                      @click="CreateShortUrl"
                    >
                      Shorten URL
                    </v-btn>
                    <v-btn
                      :disabled="!formIsValid"
                      :loading="loading.save"
                      @click="CreateShortUrl"
                      color="primary"
                      class="d-flex d-sm-none ma-auto"
                    >
                      Shorten URL
                    </v-btn>
                  </v-col>
                </v-row>
              </v-form>
              <v-row class="mt-5 mt-sm-0">
                <p class="text-body-2 d-none d-sm-flex">
                  By clicking on button you will get shortened URL
                </p></v-row
              >
            </v-col>
          </div>
        </div>
      </v-col>
    </v-row>

    <v-card>
      <v-card-title>
        URLs created befor
        <v-spacer></v-spacer>
        <v-text-field
          v-model="search"
          append-icon="mdi-magnify"
          label="Search"
          single-line
          hide-details
        ></v-text-field
      ></v-card-title>
      <v-data-table
        :headers="urlHeaders"
        :items="urlList"
        :items-per-page="20"
        :loading="loading.data"
        :search="search"
        loading-text="Trying to get data from server,We appreciate your patience"
        class="elevation-1"
      >
        <template slot="no-data">
          <v-alert border="right" color="warning" dense outlined type="warning"
            >There is no data, would youlike to try again?
            <v-btn small color="warning">Try again</v-btn>
          </v-alert>
        </template>
      </v-data-table>
    </v-card>
    <v-snackbar centered vertical v-model="snack.show" timeout="3000">
      <p>{{ snack.message }}</p>
      <p>{{snack.shortUrl}}</p>
      <template v-slot:action="{ attrs }">
        <v-btn color="blue" text v-bind="attrs" @click="showSnackbar = false">
          Close
        </v-btn>
      </template>
    </v-snackbar>
  </v-container>
</template>

<script>
export default {
  data() {
    return {
      loading: { data: false, save: false, errorLoad: false },
      formIsValid: true,
      search: "",
      urlString: "",
      inputRules: [
        (value) => !!value || "Required",
        (value) => this.isURL(value) || "URL is not valid",
      ],
      urlHeaders: [
        { text: "Date of creation", value: "dateOfCreation" },
        { text: "Atual URL", value: "actualURL" },
        { text: "Shortened URL", value: "shortenedURL" },
        { text: "Used Times", value: "timesOfView" },
      ],
      urlList: [],
      //snackbar
      snack: {
        show: false,
        message: "",
        isError: "",
        shortUrl: "",
      },
    };
  },
  mounted() {
    this.loading.data = true;
    this.LoadData();
  },
  methods: {
    isURL(str) {
      let url;

      try {
        url = new URL(str);
      } catch (_) {
        return false;
      }

      return url.protocol === "http:" || url.protocol === "https:";
    },
    LoadData() {
      this.loading.data = true;

      this.$axios
        .get("/GetAllUrls")
        .then((response) => {
          console.log(response.data);

          this.urlList = response.data;
          this.urlList.length > 0
            ? (this.noData = false)
            : (this.noData = true);
          this.loading.data = false;
          this.loading.errorLoad = false;
        })
        .catch((error) => {
          console.log(error.data);
          this.loading.data = false;
          this.loading.errorLoad = true;
        });
    },
    CreateShortUrl() {
      this.loading.save = true;
      var model = {
        ActualURL: this.urlString,
      };
      console.log(model);
      this.$axios
        .post("/create", model)
        .then((response) => {
          console.log("response is", response);

          let newItem = {
            actualURL: response.data.model.actualURL,
            dateOfCreation: response.data.model.dateOfCreation,
            shortenedURL: "https://pbid.io/" + response.data.model.shortenedURL,
            timesOfView: response.data.model.timesOfView,
          };
          this.urlList.unshift(newItem);
          this.urlString = "";
          this.loading.save = false;

          //snackabr
          this.snack.show = true;
          this.snack.message = response.data.message;
          this.snack.isError = !response.data.success;
          this.snack.shortUrl =
            "https://pbid.io/" + response.data.model.shortenedURL;
        })
        .catch((error) => {
          this.loading.save = false;
        });
    },
  },
};
</script>
