class IssuedLicense < ApplicationRecord
  self.table_name = "IssuedLicense"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :liteSeatsUnlimited, default: false
  attribute :services, default: -> { [] }
  attribute :commitUsdCents, default: 0
  attribute :overageEnabled, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :source, { backoffice: "BACKOFFICE", purchase: "PURCHASE", script: "SCRIPT", legacy_import: "LEGACY_IMPORT" },
       validate: true
  enum :seatCurrency, { usd: "USD", eur: "EUR" }, validate: { allow_nil: true }

  validates :licenseId, presence: true, uniqueness: true
  validates :tokenHash, presence: true, uniqueness: true
  validates :organizationName, presence: true
  validates :email, presence: true
  validates :planType, presence: true
  validates :maxMembers, presence: true
  validates :issuedAt, presence: true
  validates :expiresAt, presence: true
  validates :replacesId, uniqueness: { allow_nil: true }
  validates :virtualKeyId, uniqueness: { allow_nil: true }

  belongs_to :organization, foreign_key: "organizationId", optional: true, inverse_of: :issued_licenses
end
