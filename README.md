# generarCSV
## Programa para generar Codigos Seguros de Verificacion (CSV)

### Desarrollado por Carlos Clemente (11-2024)

Instrucciones:
- Se puede pasar como parametro un numero entero entre 6 y 10, que sera la longitud de la
  cadena de caracteres que se incluye en el CSV.
- Si no se pasa ningun parametro, la cadena se generará con 8 caracteres.
- El codigo generado tendra entre 18 y 22 caracteres (segun el numero que se haya pasado) y se
  incluye una referencia a la fecha y hora generados, ademas de un codigo alfanumerico aleatorio
  que garantiza la unicidad del codigo final.
- Al final del CSV se incluyen dos digitos que sirven como digitos de control para validar el codigo generado.
- Con una longitud de 8 caracteres en el codigo aleatorio, se generan alrededor de 1,5 billones
  de combinaciones posibles, por lo que aunque se generasen varios codigos en el mismo segundo,
  los caracteres aleatorios haran que sea practicamente imposible que se generen dos CSV iguales.
  
Version 1.2.1 (09-2023)
- Añadida la funcionalidad de chequeo de un CSV que se pase con el parametro -c
- Añadida ayuda por linea de comandos si se introduce el parametro -h

Version 1.3 (11-2024)
- Modificado metodo de generacion para que siempre sean trece caracteres, los diez primeros aleatorios, los dos siguientes basados en la fecha, y el ultimo es el digito de control
