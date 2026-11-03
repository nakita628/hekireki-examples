use super::space_tier::SpaceTier;
use super::space_type::SpaceType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "spaces")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    pub image: Option<String>,
    pub owner_id: String,
    #[sea_orm(default_value = "hobby")]
    pub tier: SpaceTier,
    pub space_type: Option<SpaceType>,
    pub industry: Option<String>,
    pub primary_color: Option<String>,
    #[sea_orm(default_value = false)]
    pub show_branding: bool,
    #[sea_orm(default_value = false)]
    pub hide_attribution: bool,
    #[sea_orm(default_value = false)]
    pub shared: bool,
    pub nonprofit_discount_granted_at: Option<DateTime>,
    pub created_at: DateTime,
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::OwnerId",
        to = "super::user::Column::Id",
        on_delete = "Cascade"
    )]
    Owner,
    #[sea_orm(has_many = "super::poll::Entity")]
    Polls,
    #[sea_orm(has_many = "super::scheduled_event::Entity")]
    ScheduledEvents,
    #[sea_orm(has_many = "super::event_type::Entity")]
    EventTypes,
    #[sea_orm(has_many = "super::sheet::Entity")]
    Sheets,
    #[sea_orm(has_many = "super::subscription::Entity")]
    Subscriptions,
    #[sea_orm(has_many = "super::space_member::Entity")]
    Members,
    #[sea_orm(has_many = "super::space_member_invite::Entity")]
    MemberInvites,
    #[sea_orm(has_many = "super::space_api_key::Entity")]
    ApiKeys,
    #[sea_orm(has_many = "super::nonprofit_application::Entity")]
    NonprofitApplications,
    #[sea_orm(has_many = "super::space_webhook::Entity")]
    Webhooks,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Owner.def()
    }
}

impl Related<super::poll::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Polls.def()
    }
}

impl Related<super::scheduled_event::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScheduledEvents.def()
    }
}

impl Related<super::event_type::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EventTypes.def()
    }
}

impl Related<super::sheet::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Sheets.def()
    }
}

impl Related<super::subscription::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Subscriptions.def()
    }
}

impl Related<super::space_member::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Members.def()
    }
}

impl Related<super::space_member_invite::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MemberInvites.def()
    }
}

impl Related<super::space_api_key::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ApiKeys.def()
    }
}

impl Related<super::nonprofit_application::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::NonprofitApplications.def()
    }
}

impl Related<super::space_webhook::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Webhooks.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }

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
