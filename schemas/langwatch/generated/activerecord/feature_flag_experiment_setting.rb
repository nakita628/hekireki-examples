class FeatureFlagExperimentSetting < ApplicationRecord
  self.table_name = "FeatureFlagExperimentSetting"
  self.primary_key = %w[flagKey subjectType subjectId]

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :subjectType, { user: "USER", organization: "ORGANIZATION", project: "PROJECT" }, validate: true

  validates :enabled, inclusion: { in: [ true, false ] }
end
