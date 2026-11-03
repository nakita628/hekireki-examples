class DataroomQuestionAssignment < ApplicationRecord
  self.table_name = "DataroomQuestionAssignment"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :questionId, uniqueness: { scope: :linkId, conditions: -> { where.not(linkId: nil) } }
  validates :questionId, uniqueness: { scope: :groupId, conditions: -> { where.not(groupId: nil) } }
  validates :questionId, uniqueness: { scope: :viewerId, conditions: -> { where.not(viewerId: nil) } }
  validates :email, uniqueness: { scope: :questionId, allow_nil: true }

  belongs_to :question, class_name: "DataroomQuestion", foreign_key: "questionId", inverse_of: :assignments
  belongs_to :link, foreign_key: "linkId", optional: true, inverse_of: :question_assignments
  belongs_to :group, class_name: "ViewerGroup", foreign_key: "groupId", optional: true,
                     inverse_of: :question_assignments
  belongs_to :viewer, foreign_key: "viewerId", optional: true, inverse_of: :question_assignments
end
