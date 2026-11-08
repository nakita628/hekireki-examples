class Notification < ApplicationRecord
  self.table_name = "Notification"
  self.inheritance_column = nil

  attribute :isRead, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { like: "LIKE", reply: "REPLY", mention: "MENTION", misc: "MISC", system: "SYSTEM" }, prefix: true,
                                                                                                    validate: true

  validates :url, presence: true

  belongs_to :to_user, class_name: "User", foreign_key: "toUserId", inverse_of: :to_user_notification
  belongs_to :from_user, class_name: "User", foreign_key: "fromUserId", inverse_of: :from_user_notification
  belongs_to :comment, foreign_key: "commentId", optional: true, inverse_of: :notification
  belongs_to :challenge, foreign_key: "challengeId", optional: true, inverse_of: :notification
  belongs_to :shared_solution, foreign_key: "sharedSolutionId", optional: true, inverse_of: :notification
end
