class ProgramSuggestedSet < ApplicationRecord
  attribute :id, default: -> { Cuid.generate }
  attribute :valuesInt, default: -> { [] }
  attribute :valuesSec, default: -> { [] }

  validates :setIndex, presence: true, uniqueness: { scope: :programSessionExerciseId }

  belongs_to :program_session_exercise, foreign_key: "programSessionExerciseId", inverse_of: :suggested_sets
end
