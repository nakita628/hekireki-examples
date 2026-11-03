class ApiKey < ApplicationRecord
  self.table_name = "ApiKey"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :hashedKey, presence: true, uniqueness: true

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :api_keys
  belongs_to :team, foreign_key: "teamId", optional: true, inverse_of: :api_keys
  belongs_to :app, foreign_key: "appId", optional: true, inverse_of: :api_key
  has_many :rate_limits, foreign_key: "apiKeyId", inverse_of: :api_key, dependent: :destroy
end
