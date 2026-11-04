defmodule Documenso.AvatarImage do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          bytes: String.t(),
          team: [Documenso.Team.t()],
          user: [Documenso.User.t()],
          organisation: [Documenso.Organisation.t()]
        }

  schema "AvatarImage" do
    field(:id, :string, primary_key: true)
    field(:bytes, :string)
    has_many(:team, Documenso.Team, foreign_key: :avatar_image_id, references: :id)
    has_many(:user, Documenso.User, foreign_key: :avatar_image_id, references: :id)
    has_many(:organisation, Documenso.Organisation, foreign_key: :avatar_image_id, references: :id)
  end
end