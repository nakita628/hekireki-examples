class Plugs < ApplicationRecord
  self.table_name = "Plugs"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :activated, default: true

  validates :plugFunction, presence: true, uniqueness: { scope: :integrationId }
  validates :data, presence: true

  belongs_to :integration, foreign_key: "integrationId", inverse_of: :plugs
  belongs_to :organization, foreign_key: "organizationId", inverse_of: :plugs
end
