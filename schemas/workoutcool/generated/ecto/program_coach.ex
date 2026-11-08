defmodule WorkoutCool.ProgramCoach do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          image: String.t(),
          order: integer(),
          program: WorkoutCool.Program.t() | nil
        }

  schema "program_coaches" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:image, :string)
    field(:order, :integer, default: 0)
    field(:program_id, :string, source: :programId)
    belongs_to(:program, WorkoutCool.Program, foreign_key: :program_id, define_field: false, type: :string)
  end
end