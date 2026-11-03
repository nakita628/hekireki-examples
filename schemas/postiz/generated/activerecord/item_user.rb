class ItemUser < ApplicationRecord
  self.table_name = "ItemUser"

  attribute :id, default: -> { SecureRandom.uuid }

  validates :key, presence: true, uniqueness: { scope: :userId }

  belongs_to :user, foreign_key: "userId", inverse_of: :items
end
