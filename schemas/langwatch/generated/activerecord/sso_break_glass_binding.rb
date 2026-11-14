class SsoBreakGlassBinding < ApplicationRecord
  self.table_name = "SsoBreakGlassBinding"

  attribute :warnedDays, default: -> { [] }
  alias_attribute :created_at, :createdAt

  validates :organizationId, presence: true
  validates :userId, presence: true
  validates :grantedByUserId, presence: true
  validates :grantedAt, presence: true
  validates :expiresAt, presence: true
end
