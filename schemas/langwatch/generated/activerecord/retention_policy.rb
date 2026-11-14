class RetentionPolicy < ApplicationRecord
  self.table_name = "RetentionPolicy"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :scopeType, { organization: "ORGANIZATION", team: "TEAM", project: "PROJECT" }, validate: true

  validates :organizationId, presence: true
  validates :scopeId, presence: true, uniqueness: { scope: [ :scopeType, :category ] }
  validates :category, presence: true
  validates :retentionDays, presence: true
end
