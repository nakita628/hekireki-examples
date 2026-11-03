use super::organisation_member_role::OrganisationMemberRole;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "OrganisationAuthenticationPortal")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(default_value = false)]
    pub enabled: bool,
    #[sea_orm(column_name = "clientId", default_value = "")]
    pub client_id: String,
    #[sea_orm(column_name = "clientSecret", default_value = "")]
    pub client_secret: String,
    #[sea_orm(column_name = "wellKnownUrl", default_value = "")]
    pub well_known_url: String,
    #[sea_orm(column_name = "defaultOrganisationRole", default_value = "MEMBER")]
    pub default_organisation_role: OrganisationMemberRole,
    #[sea_orm(column_name = "autoProvisionUsers", default_value = true)]
    pub auto_provision_users: bool,
    #[sea_orm(column_name = "allowedDomains")]
    pub allowed_domains: Vec<String>,
    #[sea_orm(column_name = "allowPersonalOrganisations", default_value = false)]
    pub allow_personal_organisations: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_one = "super::organisation::Entity")]
    Organisation,
}

impl Related<super::organisation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organisation.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
