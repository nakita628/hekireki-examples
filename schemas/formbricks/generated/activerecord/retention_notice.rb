class RetentionNotice < ApplicationRecord
  self.table_name = "RetentionNotice"

  attribute :id, default: -> { Cuid2.call }
  attribute :emailSent, default: false

  enum :entity, { responses: "responses", surveys: "surveys", members: "members" }, validate: true

  validates :surveyId, uniqueness: { scope: :entity, allow_nil: true }
  validates :userId, uniqueness: { scope: [ :organizationId, :entity ], allow_nil: true }

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :retention_notices
  belongs_to :survey, foreign_key: "surveyId", optional: true, inverse_of: :retention_notices
  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :retention_notices
end
