class LiteLLMLens < ApplicationRecord
  self.table_name = "LiteLLM_Lens"

  attribute :version, default: 0

  validates :data, exclusion: { in: [ nil ] }

  has_many :reviews, class_name: "LiteLLMLensReview", foreign_key: "lens_id", inverse_of: :lens, dependent: :destroy
end
