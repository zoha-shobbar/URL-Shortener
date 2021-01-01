<template>
  <v-app>
    <v-app-bar  fixed app>
      <v-icon>mdi-webpack</v-icon>
      <v-toolbar-title v-text="title" />
    </v-app-bar>
    <v-main>
      <v-container>
        <nuxt />
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
export default {
  data() {
    return {
      title: "URL Shortener",
    };
  },
};
</script>
