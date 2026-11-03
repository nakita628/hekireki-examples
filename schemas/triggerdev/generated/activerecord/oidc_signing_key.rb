class OidcSigningKey < ApplicationRecord
  self.table_name = "OidcSigningKey"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :purpose, { external_oidc: "EXTERNAL_OIDC", internal_run: "INTERNAL_RUN" }, default: :external_oidc,
                                                                                   validate: true
  enum :status, { staged: "STAGED", active: "ACTIVE", retired: "RETIRED" }, validate: true

  validates :kid, presence: true, uniqueness: true
  validates :publicJwk, exclusion: { in: [ nil ] }
  validates :readyAt, presence: true
  validates :generatedByUserId, presence: true
end
