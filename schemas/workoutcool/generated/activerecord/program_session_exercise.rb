class ProgramSessionExercise < ApplicationRecord
  attribute :id, default: -> { Cuid.generate }

  validates :order, presence: true, uniqueness: { scope: :sessionId }
  validates :instructions, presence: true
  validates :instructionsEn, presence: true
  validates :instructionsEs, presence: true
  validates :instructionsPt, presence: true
  validates :instructionsRu, presence: true
  validates :instructionsZhCn, presence: true

  belongs_to :session, class_name: "ProgramSession", foreign_key: "sessionId", inverse_of: :exercises
  belongs_to :exercise, foreign_key: "exerciseId", inverse_of: :program_session_exercise
  has_many :suggested_sets, class_name: "ProgramSuggestedSet", foreign_key: "programSessionExerciseId",
                            inverse_of: :program_session_exercise, dependent: :destroy
end
