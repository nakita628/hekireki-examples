use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Organization")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    #[sea_orm(unique)]
    pub slug: String,
    pub logo: Option<String>,
    pub metadata: Option<Json>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::member::Entity")]
    Members,
    #[sea_orm(has_many = "super::session::Entity")]
    Sessions,
    #[sea_orm(has_many = "super::invitation::Entity")]
    Invitations,
    #[sea_orm(has_many = "super::organization_rule::Entity")]
    Rules,
    #[sea_orm(has_many = "super::conversation::Entity")]
    Conversations,
    #[sea_orm(has_one = "super::sso_provider::Entity")]
    SsoProvider,
}

impl Related<super::member::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Members.def()
    }
}

impl Related<super::session::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Sessions.def()
    }
}

impl Related<super::invitation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Invitations.def()
    }
}

impl Related<super::organization_rule::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Rules.def()
    }
}

impl Related<super::conversation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Conversations.def()
    }
}

impl Related<super::sso_provider::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SsoProvider.def()
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
