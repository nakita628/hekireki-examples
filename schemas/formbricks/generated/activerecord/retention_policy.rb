class RetentionPolicy < ApplicationRecord
  self.table_name = "RetentionPolicy"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid2.call }
  attribute :enabled, default: false
  attribute :warnDays, default: 60

  enum :entity, { responses: "responses", surveys: "surveys", members: "members" }, validate: true

  validates :organizationId, uniqueness: { scope: :entity }
  validates :periodDays, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :retention_policies
  belongs_to :updated_by, class_name: "User", foreign_key: "updatedById", optional: true,
                          inverse_of: :retention_policies_updated
end
