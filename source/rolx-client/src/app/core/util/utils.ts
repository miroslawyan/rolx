export const assertDefined = (item: any, propertyName: string): void => {
  if (item[propertyName] == null) {
    console.log(item, propertyName);
    throw new Error(propertyName + ' must be defined');
  }
};
