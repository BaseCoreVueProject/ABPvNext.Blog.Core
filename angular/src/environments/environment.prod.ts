import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'Core',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44362',
    redirectUri: baseUrl,
    clientId: 'Core_App',
    responseType: 'code',
    scope: 'offline_access Core',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44315',
      rootNamespace: 'Bcvp.Blog.Core',
    },
  },
} as Environment;
