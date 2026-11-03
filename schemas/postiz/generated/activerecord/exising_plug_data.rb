class ExisingPlugData < ApplicationRecord
  self.table_name = "ExisingPlugData"

  attribute :id, default: -> { SecureRandom.uuid }

  validates :methodName, presence: true, uniqueness: { scope: [ :integrationId, :value ] }
  validates :value, presence: true

  belongs_to :integration, foreign_key: "integrationId", inverse_of: :exising_plug_data
end
