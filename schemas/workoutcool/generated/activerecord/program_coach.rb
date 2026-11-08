class ProgramCoach < ApplicationRecord
  attribute :id, default: -> { Cuid.generate }
  attribute :order, default: 0

  validates :name, presence: true
  validates :image, presence: true

  belongs_to :program, foreign_key: "programId", inverse_of: :coaches
end
