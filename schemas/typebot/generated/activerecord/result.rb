class Result < ApplicationRecord
  self.table_name = "Result"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isArchived, default: false
  alias_attribute :created_at, :createdAt

  validates :variables, exclusion: { in: [ nil ] }
  validates :isCompleted, inclusion: { in: [ true, false ] }

  belongs_to :typebot, foreign_key: "typebotId", inverse_of: :results
  has_many :answers, foreign_key: "resultId", inverse_of: :result, dependent: :destroy
  has_many :answers_v2, class_name: "AnswerV2", foreign_key: "resultId", inverse_of: :result, dependent: :destroy
  has_many :logs, foreign_key: "resultId", inverse_of: :result, dependent: :destroy
  has_many :edges, class_name: "VisitedEdge", foreign_key: "resultId", inverse_of: :result, dependent: :destroy
  has_many :set_variable_history, class_name: "SetVariableHistoryItem", foreign_key: "resultId", inverse_of: :result,
                                  dependent: :destroy
end
