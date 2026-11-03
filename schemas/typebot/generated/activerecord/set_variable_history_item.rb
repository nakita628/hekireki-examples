class SetVariableHistoryItem < ApplicationRecord
  self.table_name = "SetVariableHistoryItem"

  validates :index, presence: true, uniqueness: { scope: :resultId }
  validates :variableId, presence: true
  validates :blockId, presence: true
  validates :value, exclusion: { in: [ nil ] }

  belongs_to :result, foreign_key: "resultId", inverse_of: :set_variable_history
end
