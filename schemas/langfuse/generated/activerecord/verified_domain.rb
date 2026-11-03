class VerifiedDomain < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :verification_token, default: -> { SecureRandom.uuid }

  validates :domain, presence: true, uniqueness: { scope: :organization_id }
  validates :verification_token, uniqueness: true

  belongs_to :organization, inverse_of: :verified_domains
end
