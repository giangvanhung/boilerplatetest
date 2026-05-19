import { kamrjTemplatePage } from './app.po';

describe('kamrj App', function() {
  let page: kamrjTemplatePage;

  beforeEach(() => {
    page = new kamrjTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
