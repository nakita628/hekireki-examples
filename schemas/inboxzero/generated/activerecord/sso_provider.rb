class SsoProvider < ApplicationRecord
  self.table_name = "ssoProvider"

  attribute :id, default: -> { Cuid.generate }

  validates :issuer, presence: true
  validates :providerId, presence: true, uniqueness: true
  validates :organizationId, uniqueness: { allow_nil: true }
  validates :domain, presence: true

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :sso_providers
  belongs_to :email_account, foreign_key: "emailAccountId", optional: true, inverse_of: :ssoproviders
  belongs_to :organization, foreign_key: "organizationId", optional: true, inverse_of: :sso_provider
end
