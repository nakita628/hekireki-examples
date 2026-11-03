class Comment < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :path, default: -> { [] }
  attribute :range_start, default: -> { [] }
  attribute :range_end, default: -> { [] }

  enum :object_type, { trace: "TRACE", observation: "OBSERVATION", session: "SESSION", prompt: "PROMPT" },
       validate: true

  validates :object_id, presence: true
  validates :content, presence: true

  belongs_to :project, inverse_of: :comment
  has_many :reactions, class_name: "CommentReaction", inverse_of: :comment, dependent: :destroy
end
