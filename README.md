# SplitIt

SplitIt es una aplicación diseñada para simplificar la gestión de gastos compartidos dentro de un grupo de personas. El objetivo principal es ofrecer una visualización clara y concisa de cuánto dinero debe cada miembro del grupo y a quiénes corresponde realizar esos pagos. Es importante destacar que SplitIt se enfoca únicamente en la visualización de estas deudas y no gestiona transferencias monetarias reales.

---

## Características Principales

* **Creación de Grupos:** Permite crear grupos e incluir múltiples usuarios en ellos.
* **Registro de Gastos:** Facilita el registro de los gastos que se comparten entre los miembros del grupo.
* **Asociación de Pagos:** Permite asociar quién o quiénes realizaron el pago de cada gasto.
* **Cálculo Automático de Deudas:** Calcula automáticamente las deudas individuales generadas por cada gasto compartido.
* **Visualización Clara de Deudas:** Muestra de forma sencilla cuánto debe cada persona y a qué miembro del grupo le corresponde recibir el pago.
* **Arquitectura Limpia y Escalable:** Desarrollada siguiendo los principios de Clean Architecture para asegurar un código mantenible y escalable.

---

## Tecnologías Utilizadas

* **Framework:** .NET 9
* **Lenguaje de Programación:** C#
* **ORM:** Entity Framework Core
* **Base de Datos:** PostgreSQL
* **Contenerización:** Docker / Docker Compose

## Levantar el entorno en desarrollo

* Levantar la base de datos con el docker compose.

```bash
docker compose up -d
```

* Luego de eso, correr la aplicación desde el IDE. (En un futuro se integrara todo en el docker compose)
* Levantar las migraciones correspondientes creadas con EF y plasmarlas en la BD

```bash
dotnet ef database update --project src/SplitIt.Persistence --startup-project src/SplitIt.API
```