class LiteLLMVerifiedSubject < ApplicationRecord
  self.table_name = "LiteLLM_VerifiedSubject"
  self.primary_key = "subject_id"

  attribute :subject_id, default: -> { SecureRandom.uuid }
  attribute :kind, default: "human"
  attribute :verified_via, default: "sso_interactive"

  validates :issuer, presence: true, uniqueness: { scope: %i[tenant_id oid] }
  validates :tenant_id, presence: true
  validates :oid, presence: true

  belongs_to :user, class_name: "LiteLLMUserTable", optional: true, inverse_of: :verified_subjects
end
