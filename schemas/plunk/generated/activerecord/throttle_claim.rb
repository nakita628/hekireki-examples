class ThrottleClaim < ApplicationRecord
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :key, presence: true, uniqueness: { scope: [ :projectId, :recipientKey ] }
  validates :recipientKey, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :throttle_claims
  belongs_to :contact, foreign_key: "contactId", optional: true, inverse_of: :throttle_claims
  belongs_to :workflow, foreign_key: "workflowId", optional: true, inverse_of: :throttle_claims
end
