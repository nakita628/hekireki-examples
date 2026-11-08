class BetaTokens < ApplicationRecord
  self.table_name = "BetaTokens"

  validates :token, presence: true, uniqueness: true

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :beta_tokens
end
