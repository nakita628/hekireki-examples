class Cohort < ApplicationRecord
  attribute :definition, default: -> { JSON.parse("{}") }
  attribute :isStatic, default: false
  attribute :profileCount, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :cohorts
end
