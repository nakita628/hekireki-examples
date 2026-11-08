class Tag < ApplicationRecord
  self.table_name = "Tag"

  attribute :id, default: -> { SecureRandom.uuid }

  enum :tag, { popular: "POPULAR", newest: "NEWEST" }, validate: true

  has_and_belongs_to_many :challenges, join_table: "_ChallengeToTag", foreign_key: "B", association_foreign_key: "A"
end
