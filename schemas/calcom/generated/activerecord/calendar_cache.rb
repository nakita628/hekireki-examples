class CalendarCache < ApplicationRecord
  self.table_name = "CalendarCache"
  self.primary_key = %w[credentialId key]

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :updated_at, :updatedAt

  validates :key, uniqueness: { scope: :credentialId }
  validates :value, exclusion: { in: [ nil ] }
  validates :expiresAt, presence: true

  belongs_to :credential, foreign_key: "credentialId", optional: true, inverse_of: :calendar_cache
end
