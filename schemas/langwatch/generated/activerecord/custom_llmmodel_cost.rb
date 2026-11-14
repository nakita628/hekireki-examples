class CustomLLMModelCost < ApplicationRecord
  self.table_name = "CustomLLMModelCost"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :scopeType, { organization: "ORGANIZATION", team: "TEAM", project: "PROJECT" }, default: :project, validate: true

  validates :organizationId, presence: true
  validates :scopeId, presence: true
  validates :model, presence: true
  validates :regex, presence: true
end
