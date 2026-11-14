class ErasedIdentifierSuppression < ApplicationRecord
  self.table_name = "ErasedIdentifierSuppression"

  attribute :id, default: -> { Nanoid.generate }

  validates :organizationId, presence: true, uniqueness: { scope: [ :provider, :identifierHash ] }
  validates :provider, presence: true
  validates :identifierHash, presence: true
  validates :erasedAt, presence: true
end
