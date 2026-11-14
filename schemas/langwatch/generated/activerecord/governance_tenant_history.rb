class GovernanceTenantHistory < ApplicationRecord
  self.table_name = "GovernanceTenantHistory"

  attribute :id, default: -> { Nanoid.generate }

  validates :organizationId, presence: true, uniqueness: { scope: :tenantId }
  validates :tenantId, presence: true
  validates :firstUsedAt, presence: true
  validates :lastUsedAt, presence: true
end
