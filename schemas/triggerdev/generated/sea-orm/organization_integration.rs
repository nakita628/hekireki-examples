use super::integration_service::IntegrationService;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "OrganizationIntegration")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "friendlyId")]
    pub friendly_id: String,
    pub service: IntegrationService,
    #[sea_orm(column_name = "externalOrganizationId")]
    pub external_organization_id: Option<String>,
    #[sea_orm(column_name = "integrationData")]
    pub integration_data: Json,
    #[sea_orm(column_name = "tokenReferenceId")]
    pub token_reference_id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "deletedAt")]
    pub deleted_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::secret_reference::Entity",
        from = "Column::TokenReferenceId",
        to = "super::secret_reference::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    TokenReference,
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::OrganizationId",
        to = "super::organization::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Organization,
    #[sea_orm(has_many = "super::project_alert_channel::Entity")]
    AlertChannels,
    #[sea_orm(has_many = "super::organization_project_integration::Entity")]
    OrganizationProjectIntegration,
}

impl Related<super::secret_reference::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TokenReference.def()
    }
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::project_alert_channel::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AlertChannels.def()
    }
}

impl Related<super::organization_project_integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganizationProjectIntegration.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
