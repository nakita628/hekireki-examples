class AnswerV2 < ApplicationRecord
  self.table_name = "AnswerV2"

  alias_attribute :created_at, :createdAt

  validates :blockId, presence: true
  validates :content, presence: true

  belongs_to :result, foreign_key: "resultId", inverse_of: :answers_v2
end
