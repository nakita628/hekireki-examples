class Bookmark < ApplicationRecord
  self.table_name = "Bookmark"

  alias_attribute :created_at, :createdAt

  validates :userId, uniqueness: { scope: :challengeId, conditions: -> { where.not(challengeId: nil) } }

  belongs_to :user, foreign_key: "userId", inverse_of: :bookmark
  belongs_to :challenge, foreign_key: "challengeId", optional: true, inverse_of: :bookmark
end
