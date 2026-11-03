class Tag < ApplicationRecord
  self.table_name = "Tag"

  attribute :id, default: -> { SecureRandom.uuid }

  validates :name, presence: true, uniqueness: { scope: :userId, conditions: -> { where.not(userId: nil) } }

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :tags
  has_many :accounts, class_name: "TagsOnAccounts", foreign_key: "tagId", inverse_of: :tag, dependent: :destroy
  has_and_belongs_to_many :activities, class_name: "Order", join_table: "_OrderToTag", foreign_key: "B",
                                       association_foreign_key: "A"
end
