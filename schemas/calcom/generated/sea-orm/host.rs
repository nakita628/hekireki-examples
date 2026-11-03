use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Host")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "userId")]
    pub user_id: i32,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "eventTypeId")]
    pub event_type_id: i32,
    #[sea_orm(column_name = "isFixed", default_value = false)]
    pub is_fixed: bool,
    pub priority: Option<i32>,
    pub weight: Option<i32>,
    #[sea_orm(column_name = "weightAdjustment")]
    pub weight_adjustment: Option<i32>,
    #[sea_orm(column_name = "scheduleId")]
    pub schedule_id: Option<i32>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "groupId")]
    pub group_id: Option<String>,
    #[sea_orm(column_name = "memberId")]
    pub member_id: Option<i32>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id",
        on_delete = "Cascade"
    )]
    User,
    #[sea_orm(
        belongs_to = "super::event_type::Entity",
        from = "Column::EventTypeId",
        to = "super::event_type::Column::Id",
        on_delete = "Cascade"
    )]
    EventType,
    #[sea_orm(
        belongs_to = "super::schedule::Entity",
        from = "Column::ScheduleId",
        to = "super::schedule::Column::Id"
    )]
    Schedule,
    #[sea_orm(
        belongs_to = "super::host_group::Entity",
        from = "Column::GroupId",
        to = "super::host_group::Column::Id"
    )]
    Group,
    #[sea_orm(
        belongs_to = "super::membership::Entity",
        from = "Column::MemberId",
        to = "super::membership::Column::Id",
        on_delete = "Cascade"
    )]
    Member,
    #[sea_orm(has_one = "super::host_location::Entity")]
    Location,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::event_type::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EventType.def()
    }
}

impl Related<super::schedule::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Schedule.def()
    }
}

impl Related<super::host_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Group.def()
    }
}

impl Related<super::membership::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Member.def()
    }
}

impl Related<super::host_location::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Location.def()
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
        Ok(self)
    }
}
