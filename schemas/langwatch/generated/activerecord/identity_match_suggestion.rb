class IdentityMatchSuggestion < ApplicationRecord
  self.table_name = "IdentityMatchSuggestion"

  attribute :id, default: -> { Nanoid.generate }

  validates :organizationId, presence: true, uniqueness: { scope: [ :discoveredPersonId, :userId ] }
  validates :discoveredPersonId, presence: true
  validates :userId, presence: true
  validates :score, presence: true
  validates :computedAt, presence: true
end
