class DataroomQuestion < ApplicationRecord
  self.table_name = "DataroomQuestion"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :status, default: "unanswered"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :content, presence: true

  belongs_to :dataroom, foreign_key: "dataroomId", inverse_of: :questions
  belongs_to :team, foreign_key: "teamId", inverse_of: :dataroom_questions
  belongs_to :created_by_user, class_name: "User", foreign_key: "createdByUserId", optional: true,
                               inverse_of: :created_dataroom_questions
  belongs_to :dataroom_document, foreign_key: "dataroomDocumentId", optional: true, inverse_of: :questions
  has_many :assignments, class_name: "DataroomQuestionAssignment", foreign_key: "questionId", inverse_of: :question,
                         dependent: :destroy
  has_many :conversations, foreign_key: "questionId", inverse_of: :question, dependent: :destroy
end
