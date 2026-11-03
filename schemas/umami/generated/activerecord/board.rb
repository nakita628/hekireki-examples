class Board < ApplicationRecord
  self.table_name = "board"
  self.primary_key = "board_id"
  self.inheritance_column = nil

  validates :type, presence: true, length: { maximum: 50 }
  validates :name, presence: true, length: { maximum: 200 }
  validates :description, presence: true, length: { maximum: 500 }
  validates :parameters, exclusion: { in: [ nil ] }

  belongs_to :user, optional: true, inverse_of: :boards
  belongs_to :team, optional: true, inverse_of: :boards
end
