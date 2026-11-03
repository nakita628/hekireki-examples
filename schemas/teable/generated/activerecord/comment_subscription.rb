class CommentSubscription < ApplicationRecord
  self.table_name = "comment_subscription"

  attribute :id, default: -> { Cuid.generate }

  validates :table_id, presence: true, uniqueness: { scope: :record_id }
  validates :record_id, presence: true
  validates :created_by, presence: true
end
