class Highlight < ApplicationRecord
  self.table_name = "Highlight"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :color, presence: true
  validates :startOffset, presence: true
  validates :endOffset, presence: true
  validates :text, presence: true

  belongs_to :link, foreign_key: "linkId", inverse_of: :highlight
  belongs_to :user, foreign_key: "userId", inverse_of: :highlights
end
