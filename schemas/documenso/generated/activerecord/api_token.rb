class ApiToken < ApplicationRecord
  self.table_name = "ApiToken"

  alias_attribute :created_at, :createdAt

  enum :algorithm, { sha512: "SHA512" }, default: :sha512, validate: true

  validates :name, presence: true
  validates :token, presence: true, uniqueness: true

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :api_tokens
  belongs_to :team, foreign_key: "teamId", inverse_of: :api_tokens
end
