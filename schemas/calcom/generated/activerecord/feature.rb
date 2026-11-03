class Feature < ApplicationRecord
  self.table_name = "Feature"
  self.primary_key = "slug"
  self.inheritance_column = nil

  attribute :enabled, default: false
  attribute :stale, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { release: "RELEASE", experiment: "EXPERIMENT", operational: "OPERATIONAL", kill_switch: "KILL_SWITCH", permission: "PERMISSION" },
       default: :release, validate: { allow_nil: true }

  has_many :users, class_name: "UserFeatures", foreign_key: "featureId", inverse_of: :feature, dependent: :destroy
  has_many :teams, class_name: "TeamFeatures", foreign_key: "featureId", inverse_of: :feature, dependent: :destroy
end
