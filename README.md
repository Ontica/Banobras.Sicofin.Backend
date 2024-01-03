# Sistema de Contabilidad Financiera (SICOFIN). Banobras 2021.

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/dc6199c4794948059580c7978e17dbdd)](https://www.codacy.com/gh/Ontica/Banobras.Sicofin/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=Ontica/Banobras.Sicofin&amp;utm_campaign=Badge_Grade)
&nbsp; &nbsp;
[![Maintainability](https://api.codeclimate.com/v1/badges/a55ffb3c78e78c77e09b/maintainability)](https://codeclimate.com/github/Ontica/Banobras.Sicofin/maintainability)

La actualización del Sistema de Contabilidad Financiera (SICOFIN) está siendo construida 
a la medida de las necesidades del Banco Nacional de Obras y Servicios Públicos, S.N.C. 
(Banobras), tomando como base el diseño del mismo sistema desarrollado entre los años 
2000 y 2002 para Banobras por nuestra organización.

El proyecto está siendo desarrollado con C# 8.0, .NET Framework 4.8, ASP .NET, utilizando
como base diversos componentes de Empiria Framework y de Empiria Financial Accounting.

Al igual que la versión anterior, el sistema utiliza Oracle como servidor de base de datos,
y respeta el diseño de las mismas para evitar impactos indeseados en otros sistemas del Banco.

Este repositorio aglutina los componentes del backend del sistema y contiene código específico
para la individualización del sistema a las necesidades actuales de Banobras.

## Contenido

1.  **App Services**  
    Casos de uso con las reglas de negocio específicas para Banobras.

2.  **Web Api**  
    Consolida todas las web apis, para acceder a todos los casos de uso y componentes
    de dominio utilizados por el sistema SICOFIN.

3.  **Database Scripts**  
    Scripts con la definición de tablas, stored procedures, y otros elementos
    de la base de datos Oracle.

## Documentación

De acuerdo a las prácticas de desarrollo ágil, se escribirá e incluirá en este
repositorio en las últimas semanas del proyecto.

## Licencia

Este producto y sus partes se distribuyen mediante una licencia GNU AFFERO
GENERAL PUBLIC LICENSE, para uso exclusivo de BANOBRAS y de su personal, y
para su uso por cualquier otro organismo en México perteneciente a la
Administración Pública Federal.

Para cualquier otro uso (con excepción a lo estipulado en los Términos de
Servicio de GitHub), es indispensable obtener con nuestra organización una
licencia distinta a esta.

Lo anterior restringe la distribución, copia, modificación, almacenamiento,
instalación, compilación o cualquier otro uso del producto o de sus partes,
a terceros, empresas privadas o a su personal, sean o no proveedores de
servicios de las entidades públicas mencionadas.

El desarrollo de este producto fue pagado en su totalidad con recursos
públicos, y está protegido por las leyes nacionales e internacionales
de derechos de autor.

## Copyright

Copyright © 2021-2024. La Vía Óntica SC, Ontica LLC y autores.
Todos los derechos reservados.
