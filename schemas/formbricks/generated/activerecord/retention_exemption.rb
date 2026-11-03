class RetentionExemption < ApplicationRecord
  self.table_name = "RetentionExemption"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid2.call }

  enum :entity, { responses: "responses", surveys: "surveys", members: "members" }, validate: true
  enum :source, { manual: "manual", restore: "restore" }, default: :manual, validate: true

  validates :until, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :retention_exemptions
  belongs_to :survey, foreign_key: "surveyId", optional: true, inverse_of: :retention_exemptions
  belongs_to :created_by, class_name: "User", foreign_key: "createdById", optional: true,
                          inverse_of: :exemptions_created
  belongs_to :revoked_by, class_name: "User", foreign_key: "revokedById", optional: true,
                          inverse_of: :exemptions_revoked
end
