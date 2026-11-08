defmodule WorkoutCool.ProgramWeek do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          week_number: integer(),
          title: String.t(),
          title_en: String.t(),
          title_es: String.t(),
          title_pt: String.t(),
          title_ru: String.t(),
          title_zh_cn: String.t(),
          description: String.t(),
          description_en: String.t(),
          description_es: String.t(),
          description_pt: String.t(),
          description_ru: String.t(),
          description_zh_cn: String.t(),
          program: WorkoutCool.Program.t() | nil,
          sessions: [WorkoutCool.ProgramSession.t()]
        }

  schema "program_weeks" do
    field(:id, :string, primary_key: true)
    field(:week_number, :integer, source: :weekNumber)
    field(:title, :string)
    field(:title_en, :string, source: :titleEn)
    field(:title_es, :string, source: :titleEs)
    field(:title_pt, :string, source: :titlePt)
    field(:title_ru, :string, source: :titleRu)
    field(:title_zh_cn, :string, source: :titleZhCn)
    field(:description, :string)
    field(:description_en, :string, source: :descriptionEn)
    field(:description_es, :string, source: :descriptionEs)
    field(:description_pt, :string, source: :descriptionPt)
    field(:description_ru, :string, source: :descriptionRu)
    field(:description_zh_cn, :string, source: :descriptionZhCn)
    field(:program_id, :string, source: :programId)
    belongs_to(:program, WorkoutCool.Program, foreign_key: :program_id, define_field: false, type: :string)
    has_many(:sessions, WorkoutCool.ProgramSession, foreign_key: :week_id, references: :id)
  end
end