class Platform < ApplicationRecord
  self.table_name = "Platform"

  attribute :id, default: -> { SecureRandom.uuid }

  validates :url, presence: true, uniqueness: true

  has_many :accounts, foreign_key: "platformId", inverse_of: :platform, dependent: :nullify
end
